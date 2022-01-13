using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.Core;
using System.Data;

namespace App
{
  /// <summary>
  /// Расчет баланса на начало операции.
  /// 1. Редактор операции создает экземпляр BalanceCalc и заполняет параметры операции.
  /// 2. Вызывает метод Calculate(), который запускает отдельный поток
  /// 3. По таймеру проверяет свойство IsComplete.
  /// 4. При значении true можно поместить значения в поля редактора.
  /// Если пользователь меняет кошелек в редакторе, дату или порядок операции, создается новый объект, а старый отбрасывается.
  /// Свойство IsComplete может никогда не принять значение true, если, не задан кошелек или дата операции.
  /// </summary>
  internal class BalanceCalc
  {
    public static DBxEntry MainEntry;

    #region Исходные данные

    /// <summary>
    /// Идентификатор кошелька
    /// </summary>
    public Int32 WalletId;

    /// <summary>
    /// Идентификатор операции
    /// </summary>
    public Int32 OperationId;

    /// <summary>
    /// Дата операции
    /// </summary>
    public DateTime? Date;

    /// <summary>
    /// Порядок операции
    /// </summary>
    public int OpOrder;

    /// <summary>
    /// Тип операции также влияет на сортировку
    /// </summary>
    public OperationType OpType;

    #endregion

    #region Результаты расчета

    /// <summary>
    /// Вычисленный баланс
    /// </summary>
    public decimal Balance;

    /// <summary>
    /// Устанавливается в true, если расчет выполнен
    /// </summary>
    public volatile bool IsComplete;

    #endregion

    #region Расчет

    /// <summary>
    /// Выполнить расчет
    /// </summary>
    public void Calculate()
    {
      if (WalletId == 0)
        return;

      if (!Date.HasValue)
        return;

      Thread trd = new Thread(AsyncProc);
      trd.Start();
    }

    private void AsyncProc()
    {
      try
      {
        DoAsyncProc();
      }
      catch { }
    }

    private void DoAsyncProc()
    {
      using (DBxCon con = new DBxCon(MainEntry))
      {
        #region Суммируем операции до указанной даты

        List<DBxFilter> filters = new List<DBxFilter>();
        DBxSelectInfo si1 = new DBxSelectInfo();
        si1.TableName = "Operations";
        si1.Expressions.Add(new DBxAgregateFunction(DBxAgregateFunctionKind.Sum, "TotalDebt"), "SumDebt");
        filters.Add(new ValueFilter("Date", Date.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletDebt", WalletId));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si1.Where = AndFilter.FromList(filters);
        decimal SumDebt = DataTools.GetDecimal(con.FillSelect(si1).Rows[0][0]);

        DBxSelectInfo si2 = new DBxSelectInfo();
        si2.TableName = "Operations";
        si2.Expressions.Add(new DBxAgregateFunction(DBxAgregateFunctionKind.Sum, "TotalCredit"), "SumCredit");
        filters.Clear();
        filters.Add(new ValueFilter("Date", Date.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletCredit", WalletId));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si2.Where = AndFilter.FromList(filters);
        decimal SumCredit = DataTools.GetDecimal(con.FillSelect(si2).Rows[0][0]);

        #endregion

        #region Перебираем операции за один день

        DBxSelectInfo si3 = new DBxSelectInfo();
        si3.TableName = "Operations";
        si3.Expressions.Add("OpOrder");
        si3.Expressions.Add("Id");
        si3.Expressions.Add("WalletDebt");
        si3.Expressions.Add("WalletCredit");
        si3.Expressions.Add("TotalDebt");
        si3.Expressions.Add("TotalCredit");
        filters.Clear();
        filters.Add(new ValueFilter("Date", Date.Value));
        filters.Add(new ValueFilter("OpOrder", OpOrder, CompareKind.LessOrEqualThan));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si3.Where = AndFilter.FromList(filters);
        si3.OrderBy = DBxOrder.FromDataViewSort("OpOrder,OpOrder2,Id");
        DataTable tbl3 = con.FillSelect(si3);
        foreach (DataRow row in tbl3.Rows)
        {
          if (OperationId > 0)
          {
            if (DataTools.GetInt(row, "OpOrder") == OpOrder && DataTools.GetInt(row, "Id") == OperationId)
              break;
          }

          if (DataTools.GetInt(row, "WalletDebt") == WalletId)
            SumDebt += DataTools.GetDecimal(row, "TotalDebt");
          if (DataTools.GetInt(row, "WalletCredit") == WalletId)
            SumCredit += DataTools.GetDecimal(row, "TotalCredit");
        }

        #endregion

        Balance = SumDebt - SumCredit;
        IsComplete = true;
      }
    }

    #endregion
  }
}

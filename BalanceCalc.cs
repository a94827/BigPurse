#define USE_SUM // ����� �� ������������ SQL-������� SUM?
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
  /// ������ ������� �� ������ ��������.
  /// 1. �������� �������� ������� ��������� BalanceCalc � ��������� ��������� ��������.
  /// 2. �������� ����� Calculate(), ������� ��������� ��������� �����
  /// 3. �� ������� ��������� �������� IsComplete.
  /// 4. ��� �������� true ����� ��������� �������� � ���� ���������.
  /// ���� ������������ ������ ������� � ���������, ���� ��� ������� ��������, ��������� ����� ������, � ������ �������������.
  /// �������� IsComplete ����� ������� �� ������� �������� true, ����, �� ����� ������� ��� ���� ��������.
  /// </summary>
  internal class BalanceCalc
  {
    public static DBxEntry MainEntry;

    #region �������� ������

    /// <summary>
    /// ������������� ��������
    /// </summary>
    public Int32 WalletId;

    /// <summary>
    /// ������������� ��������
    /// </summary>
    public Int32 OperationId;

    /// <summary>
    /// ���� ��������
    /// </summary>
    public DateTime? Date;

    /// <summary>
    /// ������� ��������
    /// </summary>
    public int OpOrder;

    /// <summary>
    /// ��� �������� ����� ������ �� ����������
    /// </summary>
    public OperationType OpType;

    #endregion

    #region ���������� �������

    /// <summary>
    /// ����������� ������
    /// </summary>
    public decimal Balance;

    /// <summary>
    /// ��������������� � true, ���� ������ ��������
    /// </summary>
    public volatile bool IsComplete;

    #endregion

    #region ������

    /// <summary>
    /// ��������� ������
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
        #region ��������� �������� �� ��������� ����

        List<DBxFilter> filters = new List<DBxFilter>();
        DBxSelectInfo si1 = new DBxSelectInfo();
        si1.TableName = "Operations";
#if USE_SUM
        si1.Expressions.Add(new DBxAggregateFunction(DBxAggregateFunctionKind.Sum, "TotalDebt"), "SumDebt");
#else
        si1.Expressions.Add("TotalDebt");
#endif
        filters.Add(new ValueFilter("Date", Date.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletDebt", WalletId));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si1.Where = AndFilter.FromList(filters);
#if USE_SUM
        decimal sumDebt = DataTools.GetDecimal(con.FillSelect(si1).Rows[0][0]);
#else
        decimal sumDebt = DataTools.SumDecimal(con.FillSelect(si1), "TotalDebt");
#endif

        DBxSelectInfo si2 = new DBxSelectInfo();
        si2.TableName = "Operations";
#if USE_SUM
        si2.Expressions.Add(new DBxAggregateFunction(DBxAggregateFunctionKind.Sum, "TotalCredit"), "SumCredit");
#else
        si2.Expressions.Add("TotalCredit");
#endif
        filters.Clear();
        filters.Add(new ValueFilter("Date", Date.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletCredit", WalletId));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si2.Where = AndFilter.FromList(filters);
#if USE_SUM
        decimal sumCredit = DataTools.GetDecimal(con.FillSelect(si2).Rows[0][0]);
#else
        decimal sumCredit = DataTools.SumDecimal(con.FillSelect(si2), "TotalCredit");
#endif

        #endregion

        #region ���������� �������� �� ���� ����

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
            sumDebt += DataTools.GetDecimal(row, "TotalDebt");
          if (DataTools.GetInt(row, "WalletCredit") == WalletId)
            sumCredit += DataTools.GetDecimal(row, "TotalCredit");
        }

        #endregion

        Balance = sumDebt - sumCredit;
        IsComplete = true;
      }
    }

    #endregion
  }
}

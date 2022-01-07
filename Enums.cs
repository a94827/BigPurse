using System;
using System.Collections.Generic;
using System.Text;

namespace BigPurse
{
  #region Перечисление OperationType

  /// <summary>
  /// Типы операций
  /// </summary>
  public enum OperationType
  {
    /// <summary>
    /// Проверка баланса
    /// </summary>
    Balance = 0,

    /// <summary>
    /// Доход
    /// </summary>
    Income = 1,

    /// <summary>
    /// Расход
    /// </summary>
    Expense = 2,

    /// <summary>
    /// Перемещение между кошельками
    /// </summary>
    Move = 3,

    /// <summary>
    /// Взятие в долг
    /// </summary>
    Debt = 4,

    /// <summary>
    /// Получение в долг
    /// </summary>
    Credit = 5,
  }

  #endregion

  internal static class ProgramConvert
  {
    #region OperationType

    public static readonly string[] OperationTypeNames = new string[] {
      "Проверка баланса", 
      "Приход", 
      "Расход", 
      "Перемещение",
      "Взятие в долг", 
      "Дача в долг" };

    public static string ToString(OperationType value)
    {
      if ((int)value < 0 || (int)value >= OperationTypeNames.Length)
        return "?? " + value.ToString();
      else
        return OperationTypeNames[(int)value];
    }

    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Collections;

namespace App
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
    /// Дача в долг
    /// </summary>
    Credit = 5,
  }

  #endregion

  #region PresenceType

  /// <summary>
  /// Необходимость использования единицы измерения, описания в записях
  /// </summary>
  public enum PresenceType
  {
    /// <summary>
    /// Необходимость определяется родительским элементом
    /// </summary>
    Inherited = 0,

    /// <summary>
    /// Значение не должно задаваться
    /// </summary>
    Disabled = 1,

    /// <summary>
    /// Значение может быть задано, а может отсутствовать
    /// </summary>
    Optional = 2,

    /// <summary>
    /// Значение может отсутствовать, но выдается предупреждение
    /// </summary>
    WarningIfNone = 3,

    /// <summary>
    /// Значение является обязательным
    /// </summary>
    Required = 4,
  }

  #endregion

  internal static class Tools
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

    #region PresenceType

    public static readonly string[] PresenceTypeNames = new string[] { 
      "Унаследовано",
      "Не задается",
      "Может задаваться или нет",
      "Предупреждение, если не задано",
      "Должно быть задано"
    };

    public static string ToString(PresenceType value)
    {
      if ((int)value < 0 || (int)value >= PresenceTypeNames.Length)
        return "?? " + value.ToString();
      else
        return PresenceTypeNames[(int)value];
    }


    #endregion

    #region Operations

    /// <summary>
    /// Формат для денежных сумм
    /// </summary>
    public const string MoneyFormat = "#,##0.00";

    public static bool UseDebt(OperationType opType)
    {
      switch (opType)
      {
        case OperationType.Balance:
        case OperationType.Income:
        case OperationType.Move:
        case OperationType.Debt:
          return true;
        default:
          return false;
      }
    }

    public static bool UseCredit(OperationType opType)
    {
      switch (opType)
      {
        case OperationType.Expense:
        case OperationType.Move:
        case OperationType.Credit:
          return true;
        default:
          return false;
      }
    }

    public static int GetOpOrder2(OperationType opType)
    {
      if (opType == OperationType.Balance)
        return 2;
      else
        return 1;
    }

    #endregion
  }
}

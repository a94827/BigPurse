using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Collections;

namespace App
{
  #region ������������ OperationType

  /// <summary>
  /// ���� ��������
  /// </summary>
  public enum OperationType
  {
    /// <summary>
    /// �������� �������
    /// </summary>
    Balance = 0,

    /// <summary>
    /// �����
    /// </summary>
    Income = 1,

    /// <summary>
    /// ������
    /// </summary>
    Expense = 2,

    /// <summary>
    /// ����������� ����� ����������
    /// </summary>
    Move = 3,

    /// <summary>
    /// ������ � ����
    /// </summary>
    Debt = 4,

    /// <summary>
    /// ���� � ����
    /// </summary>
    Credit = 5,
  }

  #endregion

  #region PresenceType

  /// <summary>
  /// ������������� ������������� ������� ���������, �������� � �������
  /// </summary>
  public enum PresenceType
  {
    /// <summary>
    /// ������������� ������������ ������������ ���������
    /// </summary>
    Inherited = 0,

    /// <summary>
    /// �������� �� ������ ����������
    /// </summary>
    Disabled = 1,

    /// <summary>
    /// �������� ����� ���� ������, � ����� �������������
    /// </summary>
    Optional = 2,

    /// <summary>
    /// �������� ����� �������������, �� �������� ��������������
    /// </summary>
    WarningIfNone = 3,

    /// <summary>
    /// �������� �������� ������������
    /// </summary>
    Required = 4,
  }

  #endregion

  internal static class Tools
  {
    #region OperationType

    public static readonly string[] OperationTypeNames = new string[] {
      "�������� �������", 
      "������", 
      "������", 
      "�����������",
      "������ � ����", 
      "���� � ����" };

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
      "������������",
      "�� ��������",
      "����� ���������� ��� ���",
      "��������������, ���� �� ������",
      "������ ���� ������"
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
    /// ������ ��� �������� ����
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

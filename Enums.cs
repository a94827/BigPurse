using System;
using System.Collections.Generic;
using System.Text;

namespace BigPurse
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
    /// ��������� � ����
    /// </summary>
    Credit = 5,
  }

  #endregion

  internal static class ProgramConvert
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
  }
}

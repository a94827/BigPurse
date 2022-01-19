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
    /// ��������� � ����
    /// </summary>
    Credit = 5,
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

    #region ������� ���������

    /// <summary>
    /// ���� - ������������ ������� ���������.
    /// �������� - ���������� �������
    /// </summary>
    private static readonly TypedStringDictionary<string> _BadUnitPairs;

    /// <summary>
    /// ������ ��� ������ ������ ��������� � ���������� ��������
    /// </summary>
    private static readonly SingleScopeStringList _GoodUnits;

    /// <summary>
    /// ���������� ��� _GoodUnits
    /// </summary>
    private static readonly StringArrayIndexer _GoodUnitIndexer;

    static Tools()
    {
      #region ������������ �������

      _BadUnitPairs = new TypedStringDictionary<string>(true);
      // �� ������ ���� �����
      _BadUnitPairs.Add("�.", "�");
      _BadUnitPairs.Add("��.", "��");

      _BadUnitPairs.Add("��.", "��");
      _BadUnitPairs.Add("c�.", "��");
      _BadUnitPairs.Add("��.", "��");
      _BadUnitPairs.Add("�.", "�");
      _BadUnitPairs.Add("��.", "��");

      // ������ ���� �����
      _BadUnitPairs.Add("��", "��.");
      _BadUnitPairs.Add("��", "��.");

      // ������ �������� �� ������ ��������������
      _BadUnitPairs.Add("�����", "��.");
      _BadUnitPairs.Add("�����", "��.");
      _BadUnitPairs.Add("����", "��.");

      _BadUnitPairs.Add("����", "��.");
      _BadUnitPairs.Add("����.", "��.");
      _BadUnitPairs.Add("������", "��.");
      _BadUnitPairs.Add("������.", "��.");
      _BadUnitPairs.Add("��������", "��.");
      _BadUnitPairs.Add("��������", "��.");
      _BadUnitPairs.Add("��������", "��.");

      #endregion

      #region ���������� �������

      _GoodUnits = new SingleScopeStringList(true);
      foreach (KeyValuePair<string, string> pair in _BadUnitPairs)
        _GoodUnits.Add(pair.Value);

      _GoodUnitIndexer = new StringArrayIndexer(_GoodUnits, true);

      #endregion
    }

    /// <summary>
    /// �������� ������������ ������� ���������.
    /// ������ ������� ("") ��������� ����������
    /// </summary>
    /// <param name="s">����������� �����</param>
    /// <param name="errorText">���� ���������� ����� ��������� �� ������</param>
    /// <returns>true, ���� ����� ����������</returns>
    public static bool IsValidUnit(string s, out string errorText)
    {
      if (String.IsNullOrEmpty(s))
      {
        errorText = null;
        return true;
      }

      #region �������� ��������

      if (s[0] == ' ' || s[s.Length - 1] == ' ')
      {
        errorText = "�� ����� ���������� ��� ������������� ��������";
        return false;
      }
      if (s.IndexOf("  ") >= 0)
      {
        errorText = "��� ������� ������";
        return false;
      }

      if (s[0] == '.')
      {
        errorText = "�� ����� ���������� � �����";
        return false;
      }
      if (s.IndexOf("..") >= 0)
      {
        errorText = "��� ����� ������";
        return false;
      }

      #endregion

      #region �������� ���������� ������

      string sGood;
      if (_BadUnitPairs.TryGetValue(s, out sGood))
      {
        errorText = "������������ �������. ������ ��������������: \"" + sGood + "\"";
        return false;
      }

      int p = _GoodUnitIndexer.IndexOf(s);
      if (p >= 0)
      {
        sGood = _GoodUnits[p];
        if (!String.Equals(s, sGood, StringComparison.Ordinal))
        {
          errorText = "������������ ������� ��������. ������ �������������� \"" + sGood + "\"";
          return false;
        }
      }

      #endregion

      errorText = null;
      return true;
    }

    /// <summary>
    /// �������� ������������ ������� ���������.
    /// ������ ������� ("") ��������� ����������
    /// </summary>
    /// <param name="s">����������� �����</param>
    /// <returns>true, ���� ����� ����������</returns>
    public static bool IsValidUnit(string s)
    {
      string errorText;
      return IsValidUnit(s, out errorText);
    }

    #endregion
  }
}

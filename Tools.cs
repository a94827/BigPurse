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
    /// Получение в долг
    /// </summary>
    Credit = 5,
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

    #region Единица измерения

    /// <summary>
    /// Ключ - неправильная единица измерения.
    /// Значение - правильная единица
    /// </summary>
    private static readonly TypedStringDictionary<string> _BadUnitPairs;

    /// <summary>
    /// Список для поиска единиц измерения в правильном регистре
    /// </summary>
    private static readonly SingleScopeStringList _GoodUnits;

    /// <summary>
    /// Индексатор для _GoodUnits
    /// </summary>
    private static readonly StringArrayIndexer _GoodUnitIndexer;

    static Tools()
    {
      #region Неправильные единицы

      _BadUnitPairs = new TypedStringDictionary<string>(true);
      // Не должно быть точки
      _BadUnitPairs.Add("г.", "г");
      _BadUnitPairs.Add("кг.", "кг");

      _BadUnitPairs.Add("мм.", "мм");
      _BadUnitPairs.Add("cм.", "см");
      _BadUnitPairs.Add("дм.", "дм");
      _BadUnitPairs.Add("м.", "м");
      _BadUnitPairs.Add("км.", "км");

      // Должна быть точка
      _BadUnitPairs.Add("шт", "шт.");
      _BadUnitPairs.Add("уп", "уп.");

      // Полные названия не должны использоваться
      _BadUnitPairs.Add("штука", "шт.");
      _BadUnitPairs.Add("штуки", "шт.");
      _BadUnitPairs.Add("штук", "шт.");

      _BadUnitPairs.Add("упак", "уп.");
      _BadUnitPairs.Add("упак.", "уп.");
      _BadUnitPairs.Add("упаков", "уп.");
      _BadUnitPairs.Add("упаков.", "уп.");
      _BadUnitPairs.Add("упаковка", "уп.");
      _BadUnitPairs.Add("упаковок", "уп.");
      _BadUnitPairs.Add("упаковки", "уп.");

      #endregion

      #region Правильный словарь

      _GoodUnits = new SingleScopeStringList(true);
      foreach (KeyValuePair<string, string> pair in _BadUnitPairs)
        _GoodUnits.Add(pair.Value);

      _GoodUnitIndexer = new StringArrayIndexer(_GoodUnits, true);

      #endregion
    }

    /// <summary>
    /// Проверка корректности единицы измерения.
    /// Пустая единица ("") считается правильной
    /// </summary>
    /// <param name="s">Проверяемый текст</param>
    /// <param name="errorText">Сюда помещается текст сообщения об ошибке</param>
    /// <returns>true, если текст корректный</returns>
    public static bool IsValidUnit(string s, out string errorText)
    {
      if (String.IsNullOrEmpty(s))
      {
        errorText = null;
        return true;
      }

      #region Проверка символов

      if (s[0] == ' ' || s[s.Length - 1] == ' ')
      {
        errorText = "Не может начинаться или заканчиваться пробелом";
        return false;
      }
      if (s.IndexOf("  ") >= 0)
      {
        errorText = "Два пробела подряд";
        return false;
      }

      if (s[0] == '.')
      {
        errorText = "Не может начинаться с точки";
        return false;
      }
      if (s.IndexOf("..") >= 0)
      {
        errorText = "Две точки подряд";
        return false;
      }

      #endregion

      #region Проверка конкретных единиц

      string sGood;
      if (_BadUnitPairs.TryGetValue(s, out sGood))
      {
        errorText = "Неправильная единица. Должна использоваться: \"" + sGood + "\"";
        return false;
      }

      int p = _GoodUnitIndexer.IndexOf(s);
      if (p >= 0)
      {
        sGood = _GoodUnits[p];
        if (!String.Equals(s, sGood, StringComparison.Ordinal))
        {
          errorText = "Неправильный регистр символов. Должна использоваться \"" + sGood + "\"";
          return false;
        }
      }

      #endregion

      errorText = null;
      return true;
    }

    /// <summary>
    /// Проверка корректности единицы измерения.
    /// Пустая единица ("") считается правильной
    /// </summary>
    /// <param name="s">Проверяемый текст</param>
    /// <returns>true, если текст корректный</returns>
    public static bool IsValidUnit(string s)
    {
      string errorText;
      return IsValidUnit(s, out errorText);
    }

    #endregion
  }
}

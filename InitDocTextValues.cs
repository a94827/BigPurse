using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Data.Docs;
using FreeLibSet.Data;
using FreeLibSet.Calendar;

namespace BigPurse
{
  /// <summary>
  /// Инициализация текстового представления для документов
  /// </summary>
  internal static class InitDocTextValues
  {
    #region Основной метод

    public static void Init(DBxDocTextHandlers TextHandlers)
    {
      TextHandlers.Add("Operations", "DisplayName");
      TextHandlers.Add("OperationProducts", "Product.Name");
      TextHandlers.Add("Wallets", "Name");
      TextHandlers.Add("IncomeSources", "Name");
      TextHandlers.Add("Shops", "Name");
      TextHandlers.Add("Products", "Name");
      TextHandlers.Add("Debtors", "Name");
    }

    #endregion

    #region Обработчики для документов

    #endregion
  }
}

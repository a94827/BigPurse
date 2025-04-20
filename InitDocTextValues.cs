using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Data.Docs;
using FreeLibSet.Data;
using FreeLibSet.Calendar;

namespace App
{
  /// <summary>
  /// Инициализация текстового представления для документов
  /// </summary>
  internal static class InitDocTextValues
  {
    #region Основной метод

    public static void Init(DBxDocTextHandlers textHandlers)
    {
      textHandlers.Add("Operations", "DisplayName");
      textHandlers.Add("OperationProducts", "Product.Name");
      textHandlers.Add("Wallets", "Name");
      textHandlers.Add("IncomeSources", "Name");
      textHandlers.Add("Shops", "Name");
      textHandlers.Add("Products", "Name");
      textHandlers.Add("ProductMUSets", "MU1.Name,MU2.Name,MU3.Name");
      textHandlers.Add("Debtors", "Name");
      textHandlers.Add("MUs", "Name");
      textHandlers.Add("Purposes", "Name");
      textHandlers.Add("AuxPurposes", "Name");
    }

    #endregion

    #region Обработчики для документов

    #endregion
  }
}

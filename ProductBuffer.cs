using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Core;
using FreeLibSet.Forms;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.UICore;
using System.Data;

namespace App
{
  /// <summary>
  /// ����������� ��������� �������� ����� ��� ������ �� �������
  /// </summary>
  internal static class ProductBuffer
  {
    #region ���������� �� ������ �������

    /// <summary>
    /// ���� ���������� ������ ���������
    /// </summary>
    public struct MUSet
    {
      #region ����

      public Int32 MUId1;
      public Int32 MUId2;
      public Int32 MUId3;

      #endregion
    }

    public class ProductData : ICloneable
    {
      #region ����

      /// <summary>
      /// ������� ���� "��������"
      /// </summary>
      public PresenceType DescriptionPresence;

      /// <summary>
      /// ������� ���� "����������"
      /// </summary>
      public PresenceType PurposePresence;

      /// <summary>
      /// ������� ���������� � ������ ���������
      /// </summary>
      public PresenceType QuantityPresence;

      /// <summary>
      /// ������ ���������� ������ ���������
      /// ���� ����� ������ ����� 0, �� ����� ������������ ����� ������� �� �����������
      /// </summary>
      public MUSet[] MUSets;

      /// <summary>
      /// ������������ ���������� ������ ���������, ������� ����� ������������ (0,1,2 ��� 3)
      /// </summary>
      public int MaxQuantityLevel;

      #endregion

      #region ICloneable Members

      public ProductData Clone()
      {
        ProductData res = new ProductData();
        res.DescriptionPresence = this.DescriptionPresence;
        res.PurposePresence = this.PurposePresence;
        res.QuantityPresence = this.QuantityPresence;
        res.MUSets = this.MUSets;
        res.MaxQuantityLevel = this.MaxQuantityLevel;
        return res;
      }

      object ICloneable.Clone()
      {
        return Clone();
      }

      #endregion
    }

    private static ProductData _ZeroProductData = CreateZeroProductData();

    private static ProductData CreateZeroProductData()
    {
      ProductData obj = new ProductData();
      obj.DescriptionPresence = PresenceType.Optional;
      obj.PurposePresence = PresenceType.Optional;
      obj.QuantityPresence = PresenceType.Optional;
      obj.MUSets = new MUSet[0];
      obj.MaxQuantityLevel = 3;
      return obj;
    }

    private static Dictionary<Int32, ProductData> _ProductDict = new Dictionary<Int32, ProductData>();

    public static ProductData GetProductData(Int32 productId)
    {
      if (productId == 0)
        return _ZeroProductData;


      ProductData pd;
      if (!_ProductDict.TryGetValue(productId, out pd))
      {
        EFPApp.BeginWait("�������� ������ ���������");
        try
        {
          IdList treeIds = new IdList(); // ��� ������������ ������������ ������
          pd = DoInitProductData(productId, treeIds);
        }
        finally
        {
          EFPApp.EndWait();
        }
        // ��������� � ������� �� ���� - ��� ��� �������
      }
      return pd;
    }

    /// <summary>
    /// ����������� �����
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="treeIds"></param>
    /// <returns></returns>
    private static ProductData DoInitProductData(Int32 productId, IdList treeIds)
    {
      //if (productId == 0)
      //  return _ZeroProductData;
      if (treeIds.Contains(productId))
      {
        _ProductDict[productId] = _ZeroProductData;
        return _ZeroProductData; // �������������� ������������
      }

      object[] vals = ProgramDBUI.TheUI.DocProvider.GetValues("Products", productId,
        //                 0              1                 2               3
        new DBxColumns("ParentId,DescriptionPresence,PurposePresence,QuantityPresence"));

      ProductData parentData;
      Int32 parentId = DataTools.GetInt(vals[0]);
      if (parentId == 0)
        parentData = _ZeroProductData;
      else
        parentData = DoInitProductData(parentId, treeIds);

      ProductData pd = parentData.Clone();
      PresenceType descripionPresence = DataTools.GetEnum<PresenceType>(vals[1]);
      if (descripionPresence != PresenceType.Inherited)
        pd.DescriptionPresence = descripionPresence;

      PresenceType purposePresence = DataTools.GetEnum<PresenceType>(vals[2]);
      if (purposePresence != PresenceType.Inherited)
        pd.PurposePresence = purposePresence;

      PresenceType quantityPresence = DataTools.GetEnum<PresenceType>(vals[3]);
      if (quantityPresence != PresenceType.Inherited)
        pd.QuantityPresence = quantityPresence;

      DataTable tbl;
      tbl = ProgramDBUI.TheUI.DocProvider.FillSelect("ProductMUSets", new DBxColumns("MU1,MU2,MU3"),
        new AndFilter(new ValueFilter("DocId", productId), DBSSubDocType.DeletedFalseFilter));
      if (tbl.Rows.Count > 0)
      {
        pd.MUSets = new MUSet[tbl.Rows.Count];
        for (int i = 0; i < tbl.Rows.Count; i++)
        {
          pd.MUSets[i].MUId1 = DataTools.GetInt(tbl.Rows[i], "MU1");
          pd.MUSets[i].MUId2 = DataTools.GetInt(tbl.Rows[i], "MU2");
          pd.MUSets[i].MUId3 = DataTools.GetInt(tbl.Rows[i], "MU3");
        }
      }

#if DEBUG
      if (pd.DescriptionPresence == PresenceType.Inherited)
        throw new BugException("DescriptionPresence");
      if (pd.PurposePresence == PresenceType.Inherited)
        throw new BugException("PurposePresence");
      if (pd.QuantityPresence == PresenceType.Inherited)
        throw new BugException("QuantutyPresence");
#endif

      if (pd.QuantityPresence == PresenceType.Disabled)
        pd.MaxQuantityLevel = 0;
      else if (pd.MUSets.Length == 0)
        pd.MaxQuantityLevel = 3;
      else
      {
        pd.MaxQuantityLevel = 1;
        for (int i = 0; i < pd.MUSets.Length; i++)
        {
          int lvl = 1;
          if (pd.MUSets[i].MUId3 != 0)
            lvl = 3;
          else if (pd.MUSets[i].MUId2 != 0)
            lvl = 2;
          pd.MaxQuantityLevel = Math.Max(pd.MaxQuantityLevel, lvl);
        }
      }

      _ProductDict[productId] = pd;
      return pd;
    }

    public static void ResetProductData()
    {
      _ProductDict.Clear();
    }

    /// <summary>
    /// ���������� true, ���� ���� �������� ��� �����
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool GetColumnEnabled(Int32 productId, string columnName)
    {
      ProductData pd = GetProductData(productId);
      switch (columnName)
      {
        case "Description":
          return pd.DescriptionPresence != PresenceType.Disabled;
        case "Purpose":
          return pd.PurposePresence != PresenceType.Disabled;
        case "Quantity1":
        case "MU1":
          return pd.MaxQuantityLevel >= 1;
        case "Quantity2":
        case "MU2":
          return pd.MaxQuantityLevel >= 2;
        case "Quantity3":
        case "MU3":
          return pd.MaxQuantityLevel >= 3;
        default:
          throw new ArgumentOutOfRangeException("columnName", columnName, "������������ ��� ����");
      }
    }

    public static string[] GetOpProductValues(Int32 productId, string columnName)
    {
      ProductData pd = GetProductData(productId);
      PresenceType prs;
      //Int32[] fixedList;
      switch (columnName)
      {
        case "Description":
          prs = pd.DescriptionPresence;
          //fixedList = null;
          break;
        //case "MU1":
        //  prs = pd.Unit1Presence;
        //  fixedList = pd.MU1List;
        //  break;
        //case "MU2":
        //  prs = pd.Unit2Presence;
        //  fixedList = pd.MU2List;
        //  break;
        default:
          throw new ArgumentOutOfRangeException("columnName", columnName, "������������ ��� ����");
      }

      if (prs == PresenceType.Disabled)
        return DataTools.EmptyStrings;
      //if (fixedList == null)
      return DoGetOpProductValues(productId, columnName); // �������
      //else
      //  return fixedList;
    }


    public static void ValidateProductDescription(Int32 productId, string value, IUIValidableObject args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      ProductData pd = GetProductData(productId);

      if (String.IsNullOrEmpty(value))
      {
        switch (pd.DescriptionPresence)
        {
          case PresenceType.Required:
            args.SetError("���� ������ ���� ���������");
            break;
          case PresenceType.WarningIfNone:
            args.SetWarning("���� ������ ������ ���� ���������");
            break;
        }
        return;
      }

      if (pd.DescriptionPresence == PresenceType.Disabled)
      {
        args.SetError("���� �� ������ �����������");
        return;
      }
    }

    public static void ValidateProductPurpose(Int32 productId, Int32 purposeId, IUIValidableObject args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      ProductData pd = GetProductData(productId);

      if (purposeId == 0)
      {
        switch (pd.PurposePresence)
        {
          case PresenceType.Required:
            args.SetError("���������� ������ ���� �������");
            break;
          case PresenceType.WarningIfNone:
            args.SetWarning("���������� ������ ������ ���� �������");
            break;
        }
        return;
      }

      if (pd.PurposePresence == PresenceType.Disabled)
      {
        args.SetError("���������� �� ������ �����������");
        return;
      }
    }

    #endregion

    #region ������ ����� ���������������� ��������

    /// <summary>
    /// ������� ��������.
    /// ���� "ProductId|�������".
    /// �������� - ��������������� ������ ����� ��� ������.
    /// </summary>
    private static Dictionary<string, string[]> _OpProductDict = new Dictionary<string, string[]>();

    private static string GetOpProductKey(Int32 productId, string columnName)
    {
      return StdConvert.ToString(productId) + "|" + columnName;
    }

    /// <summary>
    /// �������� �������� ��� ����������� ������ � ��������� ������������ "OperationProducts"
    /// </summary>
    /// <param name="productId">��������� ������������� ������</param>
    /// <param name="columnName">��� ���� ("Description", "Unit1" ��� "Unit2")</param>
    /// <returns>������ ��� ������ ��� ������ ������</returns>
    private static string[] DoGetOpProductValues(Int32 productId, string columnName)
    {
      if (productId == 0)
        return DataTools.EmptyStrings;

      string[] a;
      if (!_OpProductDict.TryGetValue(GetOpProductKey(productId, columnName), out a))
      {
        EFPApp.BeginWait("��������� ������ ��������");
        try
        {
          DBxFilter[] filters = new DBxFilter[3];
          filters[0] = new ValueFilter("Product", productId);
          filters[1] = DBSSubDocType.DeletedFalseFilter;
          filters[2] = DBSSubDocType.DocIdDeletedFalseFilter;
          a = ProgramDBUI.TheUI.DocProvider.GetUniqueStringValues("OperationProducts", columnName,
            AndFilter.FromArray(filters));

          #region ��������� ������ ���������� �������

          /*
          List<string> lst = null;
          for (int i = 0; i < a.Length; i++)
          {
            if (Tools.IsValidUnit(a[i]))
            {
              if (lst != null)
                lst.Add(a[i]);
            }
            else
            {
              if (lst == null)
              {
                lst = new List<string>(a.Length - 1);
                for (int j = 0; j < i; j++)
                  lst.Add(a[j]);
              }
            }
          }

          if (lst != null)
            a = lst.ToArray();
          */
          #endregion
        }
        finally
        {
          EFPApp.EndWait();
        }

        _OpProductDict[GetOpProductKey(productId, columnName)] = a;
      }
      return a;
    }

    public static void AddOpProductValues(Int32 productId, string columnName, string value)
    {
      if (productId == 0)
        return;
      if (String.IsNullOrEmpty(value))
        return;
      string[] a1;
      if (!_OpProductDict.TryGetValue(GetOpProductKey(productId, columnName), out a1))
        return; // �� ��������� ������, � �� ����.

      if (Array.IndexOf<string>(a1, value) >= 0)
        return; // ��� ����

      string[] a2 = a1;
      Array.Resize<string>(ref a2, a1.Length + 1);
      a2[a2.Length - 1] = value;
      Array.Sort<string>(a2);
      _OpProductDict[GetOpProductKey(productId, columnName)] = a2;
    }

    #endregion
  }
}

﻿using Nop.Core.Domain.Localization;

namespace Nop.Services.ExportImport
{
    /// <summary>
    /// Import manager interface
    /// </summary>
    public interface IImportManager
    {
        /// <summary>
        /// Import products from XLSX file
        /// </summary>
        /// <param name="filePath">Excel file path</param>
        void ImportProductsFromXlsx(string filePath);
        
        /// <summary>
        /// Import language resources from XML file
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="xml">XML</param>
        void ImportLanguageFromXml(Language language, string xml);
    }
}

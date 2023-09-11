using WareHousePlatForm.Data;

namespace WareHousePlatForm.Models
{
    public class HistoryProduct
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public List<BoundData> Bound { get; set; }
        public List<ImportItem> importItems { get; set; }
        public List<ExportHistory> exports { get; set; }
    }
}

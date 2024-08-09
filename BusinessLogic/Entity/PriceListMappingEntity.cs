using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    public class PriceListMappingEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("company_price_list_id")]
        public int CompanyPriceListId { get; set; }
        [Column("stock_identification_id")]
        public int StockIdentificationId { get; set; }
        [Column("company_definition_id")]
        public int CompanyDefinitionId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }


    }
}
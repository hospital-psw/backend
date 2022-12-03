using IntegrationLibrary.Core;
using System;
using IntegrationLibrary.Tender.Enums;
using System.Collections.Generic;


namespace IntegrationLibrary.Tender

{
    public class Tender : Entity
    {
        public TenderStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<TenderOffer> Offers { get; set; }
        public TenderOffer TenderWinner { get; set;}
        public List<TenderItem> Items { get; set; }  
    }
}

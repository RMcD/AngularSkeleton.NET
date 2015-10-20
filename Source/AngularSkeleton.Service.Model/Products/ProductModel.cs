using System;
using System.Runtime.Serialization;

namespace AngularSkeleton.Service.Model.Products
{
    /// <summary>
    ///     Models a product
    /// </summary>
    public class ProductModel : ModelBase
    {
        /// <summary>
        ///     The product id
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        ///     The product friendly name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     The product description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        ///     The quantity available
        /// </summary>
        [DataMember]
        public int QuantityAvailable { get; set; }

        /// <summary>
        ///     The date the product was added
        /// </summary>
        [DataMember]
        public DateTimeOffset DateAdded { get; set; }
    }
}
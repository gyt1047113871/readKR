﻿namespace KRCON.Data.Models
{
    using RCL.Core;
    using RCL.Data.NH;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class KRT_MY_SHIP : DataEntityBase<int>
    {
        public override int GetHashCode()
        {
            if (this.IsSaved)
            {
                return base.GetHashCode();
            }
            return this.Id.GetHashCode();
        }

        public override string ToString() => 
            this.ObjectToString();

        public virtual string CATEGORY_NAME { get; set; }

        public virtual DateTime? CONSTRUCTION_DATE { get; set; }

        public virtual DateTime? CURRENT_DATE { get; set; }

        public virtual int? GROSS_TONNAGE { get; set; }

        public virtual string LOCALE_KEY { get; set; }

        public virtual DateTime? REG_DATE { get; set; }

        public virtual string REG_ID { get; set; }

        public virtual int? SHIP_LENGTH { get; set; }

        public virtual string SHIP_TYPE { get; set; }
    }
}


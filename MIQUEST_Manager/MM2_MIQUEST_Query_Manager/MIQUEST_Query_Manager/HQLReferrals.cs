﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIQUEST_Query_Manager
{
    class HQLReferrals
    {
        public const string Name = "REF";

        public string CreateReportSubset(string Subset)
        {
            string sBuffer = "";

            sBuffer = "FOR " + Subset + "\r\n";
            sBuffer = sBuffer + "SUBSET " + Name + GlobalDefs.cSubsetExt + " TEMP" + "\r\n";
            sBuffer = sBuffer + "FROM REFERRALS (ONE FOR PATIENT)" + "\r\n";
            sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\",\"D\",\"L\")" + "\r\n";

            return sBuffer;
        }

        public string CreateReportPrint(DateTime from, DateTime to)
        {
            string sBuffer = "";

            sBuffer = "FOR " + Name + GlobalDefs.cSubsetExt + "\r\n";
            sBuffer = sBuffer + "REPORT" + "\r\n";
            sBuffer = sBuffer + "PRINT " + GlobalDefs.ReferralFields + "\r\n";
            sBuffer = sBuffer + "FROM REFERRALS (ALL FOR PATIENT)" + "\r\n";
            sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\",\"D\",\"L\")" + "\r\n";
            sBuffer = sBuffer + "AND  DATE IN (\"" + from.ToShortDateString() + "\"-\"" + to.ToShortDateString() + "\")" + "\r\n";

            return sBuffer;
        }
    }
}

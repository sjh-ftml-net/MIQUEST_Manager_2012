using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIQUEST_Query_Manager
{
    class GlobalDefs
    {
        public const char cDelimiter = ';';
        public const char cSubsetExt = 'G';
        public const char cReportExt = 'R';

        public const string PatientFields = "PCG,PRACTICE,PRACT_NUMBER,SURGERY,NHS_NUMBER,REFERENCE,ACTIVE,SEX,DATE_OF_BIRTH,POSTCODE,GP,GP_USUAL,REGISTERED_DATE,REMOVED_DATE,DATE_OF_DEATH";
        public const string JournalFields = "NHS_NUMBER,REFERENCE,DATE,RECORD_DATE,CODE,HCP,HCP_TYPE,EPISODE,RUBRIC,VALUE1,VALUE2,LINKS";
        public const string EncounterFields = "NHS_NUMBER,REFERENCE,DATE,HCP,HCP_TYPE,SESSION,LOCATION,DURATION,LINKS";
        public const string ReferralFields = "NHS_NUMBER,REFERENCE,DATE,HCP,HCP_TYPE,TO_HCP,SPECIALITY,UNIT,TYPE,LINKS";
    }
}
# Top level export file for ACG export

# Create cohort subsets
$INCLUDE; CTV3_COHORT.inc

# Take all clincal findings and drugs

$START_JOURNAL_SET; DIA; CTV3; CURRENT; DIAGNOSES
$CODES; DIA; 1; "XaBVJ%"; Clinical Findings
$END_JOURNAL_SET

$START_JOURNAL_SET; DRG; CTV3; CURRENT; DRUGS
$CODES; DRG; 1; "x00xm%"; Drugs
$END_JOURNAL_SET





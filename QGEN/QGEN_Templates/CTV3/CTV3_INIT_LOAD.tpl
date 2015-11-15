# Top level export file for ACG export

# Create the population subgroups
# Create the SUBSETS for use in all the queries
# keywords
#	<name> is replaced with the query name.
#	<delta_period_start> is replaced with the start date of the delta period selected.
#	<active_period_start> is replaced with the start date of the active period selected
#	<ltc_period_start> is replaced with the start date of the LTC period selected.
#	<period_end> is replaced with the end date of the period selected.

# The query names must be extended in the file generation so in here they MUST be 4 or less characters

# Create cohort subsets
$INCLUDE; CTV3_COHORT.inc

# Take all clincal findings and all drugs for 1 year

# $START_JOURNAL_SET; [NAME]; [CODE TYPE]; [PERIOD]; [TYPE]
# [NAME] is name of set
# [CODE TYPE] is CTV3 or 5Byte
# [PERIOD] is CURRENT or LTC
# [TYPE] is DIAGNOSIS or DRUGS

$START_JOURNAL_SET; DIA; CTV3; CURRENT; DIAGNOSES
$CODES; DIA; 1; "XaBVJ%"; Clinical Findings
$END_JOURNAL_SET

$START_JOURNAL_SET; DRG; CTV3; CURRENT; DRUGS
$CODES; DRG; 1; "x00xm%"; Drugs
$END_JOURNAL_SET

$INCLUDE; CTV3_QOF.inc
#$INCLUDE; CTV3_LTC.inc


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

# Notes from test
# The query/subset names seem to get mangled if they include lower case so make them UPPER

# Create cohort subsets
$INCLUDE; CTV3_COHORT.inc

# Take all clincal findings, drugs, encounters and referrals

$START_JOURNAL_SET; DIA; CTV3; CURRENT; DIAGNOSES
$CODES; DIA; 1; "XaBVJ%"; Clinical Findings
$END_JOURNAL_SET

$START_JOURNAL_SET; DRG; CTV3; CURRENT; DRUGS
$CODES; DRG; 1; "x00xm%"; Drugs
$END_JOURNAL_SET

$START_JOURNAL_SET; ENC1; CTV3; CURRENT; ENCOUNTERS
$CODES; EN1; 1; "XaBTL%"; Encounters
$END_JOURNAL_SET

$START_JOURNAL_SET; REF1; CTV3; CURRENT; REFERRALS
$CODES; RE1; 1; "8H4..%","8H5..%","8H6..%","8H7..%"; Referrals 1
$END_JOURNAL_SET

$START_JOURNAL_SET; REF2; CTV3; CURRENT; REFERRALS
$CODES; RE2; 1; "8H1..%","8H2..%","8H3..%","8H33.%","8HC..%","8HD..%","8HK..%","8HP..%","8HQ..%","8HR..%","8HS..%","8HU..%"; Referrals 2
$END_JOURNAL_SET




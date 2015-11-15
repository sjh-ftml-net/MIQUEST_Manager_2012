# Top level export file for ACG export

# Create cohort subsets
$INCLUDE; 5Byte_COHORT.inc

# Take all clincal findings and drugs

$START_JOURNAL_SET; DIA; 5BYTE; CURRENT; DIAGNOSES
$CODES; DIA; 1; "A%","B%","C%","D%","E%","F%","G%","H%","J%","K%","L%","M%","N%","P%","Q%","R%","S%","T%","U%","Z%"; Diseases
$END_JOURNAL_SET

$START_JOURNAL_SET; RES; 5BYTE; CURRENT; DIAGNOSES
$CODES; RES; 1; "2126.","212P.","212R","212T.","212K.","212S.","212H.","212J."; Resolved Codes
$END_JOURNAL_SET

$START_JOURNAL_SET; DRG; 5BYTE; CURRENT; DRUGS
$CODES; DRG; 1; "a%","b%","c%","d%","e%","f%","g%","h%","i%","j%","k%","l%","m%","n%","o%","p%","q%","s%","u%"; Drugs
$END_JOURNAL_SET

$INCLUDE; 5Byte_QOF.inc

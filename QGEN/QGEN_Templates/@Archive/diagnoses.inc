# -- Export diagnoses and drugs from journals

$START_JOURNAL, CTV3, CURRENT, DIAGNOSES

$CODES, DIAG001, "Y000E%"
$CODES, DIAG002, "E....%"
$CODES, DIAG003, "Xa0tn%" 

$END_JOURNAL

$START_JOURNAL, CTV3, CURRENT, DRUGS

$CODES, DRU001, "x01By%"
$CODES, DRU002, "x01Ap%"
$CODES, DRU003, "x01QF%"

$END_JOURNAL

$START_JOURNAL, 5Byte, CURRENT, DIAGNOSES

$END_JOURNAL

$START_JOURNAL, BNF, CURRENT, DIAGNOSES

$END_JOURNAL

$START_JOURNAL, CTV3, LTC, DIAGNOSES

$CODES, LTC001,  "A1...%","G5003","G5206","Q4024","F4A55","X101S","X701a" 
$CODES, LTC002,  "A30..%"
$CODES, LTC003,  "A41..%"

$END_JOURNAL

$START_JOURNAL, 5BYTE, LTC, DIAGNOSES

$END_JOURNAL

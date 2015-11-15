# Select all the patients
$COHORT; CSPOP; Full practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
$ECOHORT

# Select patient who refused to share records
$COHORT; CSREF; Patients refused to share records
SUBSET <name> TEMP
FROM JOURNALS (LATEST FOR PATIENT)
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND CODE IN ("XaMdS","XaKII","XaQVo")
AND CHOSEN.CODE IN ("XaKII","XaQVo")
$ECOHORT

# Select ALL consented patients
$COHORT; CSALL; Patients consented to share records
$MAP; DEFAULT
FOR CSPOP NOT CSREF
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

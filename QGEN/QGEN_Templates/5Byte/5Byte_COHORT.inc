# Select all the patients
$COHORT; CSPOP; Full practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
$ECOHORT

# Select patient who refused to share records
$COHORT; CSREF; Patients refused to share records
FROM JOURNALS (LATEST FOR PATIENT)
WHERE ACTIVE IN ("R","T","S","D","L","P")
WHERE CODE IN ("93C0","93C2","93C1","93C3")
AND CHOSEN.CODE IN ("93C1","93C3")
$ECOHORT

# Select ALL consented patients
$COHORT; CSALL; Patients consented to share records
$MAP; DEFAULT
FOR CSPOP NOT CSREF
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

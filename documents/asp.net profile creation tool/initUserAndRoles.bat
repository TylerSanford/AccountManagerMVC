:: Create Roles for Users
"ikfbatool.exe" cr "Administrator"
"ikfbatool.exe" cr "Developer"
"ikfbatool.exe" cr "Internal"
"ikfbatool.exe" cr "Manager"
"ikfbatool.exe" cr "Customer"
"ikfbatool.exe" cr "User"

:: Create User
"ikfbatool.exe" cu "tyler@projex.co" "Password123" "tyler@projex.co" "question" "answer"
"ikfbatool.exe" cu "support@projex.co" "Password123" "support@projex.co" "question" "answer"



:: Add User to Roles
::"ikfbatool.exe" au "tyler@projex.co" "Developer"
"ikfbatool.exe" au "support@projex.co" "Internal"
::"ikfbatool.exe" au "tyler@projex.co" "Manager"
"ikfbatool.exe" au "tyler@projex.co" "Administrator"




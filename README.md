# ASP.NETCore
git add .
(.) represents all the untracked files. If you want to move a specific file then you can the following command.
// If you see below error then add .gitignore file
Git commit vsidx file access denied in Visual Studio
Git > Settings > Source Control > Git Repository Settings... Then added the .gitignore file which by default included the .vs folder. 

==> CREATE JWT-TOEKN WITH BEARER
        KeyPoints: 
            1. Add package Microsoft.AspNetCore.Authentication.JwtBearer 
            2. Appsettings.json==>  "Jwt": {
                                    "Key": "ThisismySecretKeyThisismySecretKey_ThisismySecretKeyThisismySecretKey",
                                    "Issuer": "Test.com"   // usually reads from Vault
                                          } 
            3. Program.cs==> builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                             .AddJwtBearer(options =>...............
            4. HomeController.cs ==> Main logic goes here to create JWTtoken
            5. In postman:
                        Key: Authorization
                        value: Bearer <token>

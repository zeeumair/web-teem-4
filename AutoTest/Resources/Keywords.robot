*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window

    Set Selenium Speed          0.0

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  class:navbar-brand
    Should Be Equal		        ${link_text}  OMGZ SHOES

Register Valid User account
    [Arguments]                  ${email_text}  ${password}
    Click Link                 xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Enter FirstName
    Enter LastName
    Enter StreetAdress
    Enter PostNumber
    Enter City
    Enter Country
    Enter Password              ${password}
    Enter Email                 ${email_text}
    Enter PhoneNumber
    Click Button                id:submit

Enter FirstName
    Input Text                  id:FirstName  ${first_name}

Enter LastName
    Input Text                  id:LastName  ${sur_name}

Enter StreetAdress
    Input Text                  id:StreetAdress  ${street_adress}

Enter PostNumber
    Input Text                  id:PostNumber  ${post_number}

Enter City
    Input Text                  id:City  ${city}

Enter Country
    Input Text                  id:Country  ${country}

Enter Password
    [Arguments]                 ${password}
    Input Text                  id:Password  ${password}

Enter Email
    [Arguments]                 ${email_text}
    Input Text                  id:EmailAddress  ${email_text}

Enter PhoneNumber
    Input Text                  id:PhoneNumber  ${phone_number}

Verify Register Loaded
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/div/form/div[10]/input
    Should Be Equal		         ${link_text}  Create

Verify Email Taken
    ${link_text} =              Get Text  xpath:/html/body/div/main/div/div/form/div[1]/ul/li[2]
    Should Be Equal             ${link_text}  Email 'alfred.josephalfred@gmail.com' is already taken.

Invalid Log in with incorrect E-mail
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Input Text                  id:Email  alfred.josephalfreddd@gmail.com
    Input Text                  id:Password  Joe_777
    Click Button                xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Verify Invalid Log in with incorrect E-mail
    Wait Until Page Contains    Invalid Login Attempt

Forgot Password with ability to reset it
    Click Link                  xpath:/html/body/div/main/div[2]/div/div[2]/a
    Input Text                  id:Email  alfred.josephalfred@gmail.com
    Click Button                xpath:/html/body/div/main/div/div/form/input[1]

Verify forgot password request is being sent
    ${link_text} =              Get Text  xpath:/html/body/div/main/h4
    Should Be Equal             ${link_text}  If you have an account with us an email with the instructions have been sent to you.

Log in with Valid E-mail
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Input Text                  id:Email  alfred.josephalfred@gmail.com
    Input Text                  id:Password  Joe_777
    Click Button                xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Verify Log in with valid E-mail
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Should Be Equal             ${link_text}  Logout

Check Min Sida after succsess Log in
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Execute Javascript          window.scrollBy(0,400)
    Click Button                xpath://*[@id="saveUserInfo"]

Verify Check Min Sida contains correct registeration's input for this User
    ${link_text} =              Get Text  id:OrderHistoryLink
    Should Be Equal             ${link_text}  View Order History

Log out from User Account
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a

Verify User is logged out
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Should Be Equal             ${link_text}  Login

Non specified User can't confirm order without login or register
    Click Element               id:AddToShoppingCart
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[1]/a
    Click Element               xpath:/html/body/div/main/div/span/a

Verify User can't confirm order without login or register
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Should Be Equal             ${link_text}  Register

End Web Test
    Close Browser


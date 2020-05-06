*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed          0.7

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  class:navbar-brand
    Should Be Equal		        ${link_text}  Webshop

Register User account
    [Arguments]                  ${email_text}  ${password}
    Click Link                 xpath:/html/body/header/nav/div/div/ul/li[3]/a[2]
    Enter FirstName
    Enter LastName
    Enter StreetAdress
    Enter PostNumber
    Enter City
    Enter Country
    Enter Password              ${password}
    Enter Email                 ${email_text}
    Enter PhoneNumber
    Click Button                xpath:/html/body/div/main/div/div/form/div[10]/input

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
    Input Text                  id:Email  ${email_text}

Enter PhoneNumber
    Input Text                  id:PhoneNumber  ${phone_number}

Verify Register Loaded
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/div/form/div[10]/input
    Should Be Equal		         ${link_text}  Create

Verify Email Taken
    ${link_text} =              Get Text  xpath:/html/body/div/main/div/div/form/div[1]/ul/li[2]
    Should Be Equal             ${link_text}  Email 'alfred.josephalfred@gmail.com' is already taken.

Verify Log in
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[3]/a[1]
    Input Text                  id:Email  alfred.josephalfred@gmail.com
    Input Text                  id:Password  Joe_777
    Click Button                xpath:/html/body/div/main/div[2]/div/form/div[3]/input
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Should Be Equal             ${link_text}  Logout    //it should be "Logout" as verification but it's not accepting it

Invalid Log in
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[3]/a[1]
    Input Text                  id:Email  alfred.josephalfreddd@gmail.com
    Input Text                  id:Password  Joe_777
    Click Button                xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Verify Invalid Log in
    Wait Until Page Contains    Invalid Login Attempt

End Web Test
    Close Browser


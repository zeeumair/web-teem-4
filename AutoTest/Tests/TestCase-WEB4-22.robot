*** Settings ***
Documentation                   This is some basic info about the whole test suite
Resource                        ../Resources/keywords.robot
Library                         SeleniumLibrary
Library                         DateTime
Suite Setup                     Begin Web Test
Suite Teardown                  End Web Test

*** Variables ***
${BROWSER} =                    chrome
${URL} =                        https://localhost:44304/
${first_name} =                 Joseph
${sur_name} =                   Alfred
${phone_number} =               0999066600
${street_adress} =              68 Värmlandsgatan 11
${post_number} =                41044
${country} =                    Sweden
${city} =                       Gothenburg

*** Test Cases ***

User can access Webshop page

    [Documentation]             See if page is accessable and can load correctly
    [Tags]                      Access Webshop main page
    Go to Web Page

User cannot create User account
    [Documentation]             Test: The user should be able to switch to Register page from the header menu to create account
    [Tags]                      User wouldn't be able to register a new User with same E-mail.
    Given Go to Web Page
    When Register User account   alfred.josephalfred@gmail.com   Joe_777
    Then Verify Email Taken

User can not Log in with invalid E-mail
    [Documentation]             Test: The user should not be able to Log in
    [Tags]                      Log in fail to User account
    Invalid Log in with incorrect E-mail
    Verify Invalid Log in with incorrect E-mail

User can reset Forgoten Password
    [Documentation]             Test:The User will be able to reset an forgoten password
    [Tags]                      Forgot Password
    Forgot Password with ability to reset it
    Verify forgot password request is being sent

User can Log in with valid E-mail
    [Documentation]             Test: The user should be able to Log in after being registered
    [Tags]                      Log in to User account
    Log in with Valid E-mail
    Verify Log in with valid E-mail

User can check Min Sida after being logged in with Valid E-mail
    [Documentation]             Test: The User should be able to check input detaild on Min Sida after log in
    [Tags]                      Check Min Sida
    Check Min Sida after succsess Log in
    Verify Check Min Sida contains correct registeration's input for this User

User can Log out
    [Documentation]             Test: The User should be able to Log out from Min Sida and from the account
    [Tags]                      Log out
    Log out from User Account
    Verify User is logged out

User can't confirm order without login or register
    [Documentation]             Test: The user wont be able to confirm an order without being registerd or log in first
    [Tags]                      Verify Login or register to confirm order
    Non specified User can't confirm order without login or register
    Verify User can't confirm order without login or register





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
${city} =                       Göteborg

*** Test Cases ***

User can access Webshop page

    [Documentation]             See if page is accessable and can load correctly
    [Tags]                      Access Webshop main page

        Go to Web Page

User cannot create Register User account
    [Documentation]             Test: The user should be able to switch to Register page from the header menu to create account
    [Tags]                      Registeration but wouldn't be able to recreate a new User with same E-mail.

       Given Go to Web Page
       When Register User account   alfred.josephalfred@gmail.com   Joe_777
       Then Verify Email Taken

User can Log in
    [Documentation]             Test: The user should be able to Log in after being registered
    [Tags]                      Log in to User account

        Verify Log in

User can not Log in
    [Documentation]             Test: The user should not be able to Log in
    [Tags]                      Log in fail to User account

        Invalid Log in
        Verify Invalid Log in




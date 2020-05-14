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

User can Log in with Google Account
    [Documentation]             Test: The user should be able to Login with Google Account
    [Tags]                      Login to goole User account
    Login with Google Account
    Verify Login with Google Account
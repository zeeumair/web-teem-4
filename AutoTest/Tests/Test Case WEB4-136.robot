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

*** Test Cases ***

User can access OMGZ SHOES page
    [Documentation]             Test: if page is accessable and can load correctly
    [Tags]                      Access OMGZ SHOES main page
    Go to Web Page

User get a random chuck norris joke before ordering
    [Documentation]             Test: User should get a randome joke before submiting a new order
    [Tags]                      Random jokes
    Random Chuck Norris joke before ordering
    Verify Chuck Norris joke
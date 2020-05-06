*** Settings ***

Documentation                   This is some basic info about the whole Webshop test suite
Resource                        ../Resources/Keywords.robot
Library                         SeleniumLibrary
Library                         DateTime

Suite Setup                     Begin Web Test
Suite Teardown                  End Web Test

*** Variables ***

${BROWSER} =                    chrome
${URL} =                        https://localhost:44304/

*** Test Cases ***

User can access Webshop site
    [Documentation]             Test: User should be able to access the Webshop main page
    [Tags]                      Access Webshop

        Go to Web Page

User should be able to see currency converter
    [Documentation]             Test: The user should be able to see Currency Convertor button top on Webshop
    [Tags]                      Currency Convertor

         Click Element          //*[@id="currentCurrency"]

User should be able to chang prices Webshop site
    [Documentation]             Test: The user should be able to change all prices on Webshop site by slecting drop down currency
    [Tags]                      Change Price

        Slect Currency from List


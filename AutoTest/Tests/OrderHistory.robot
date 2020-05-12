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

User can access Order History page
    [Documentation]             Test: User should be able to see order history on Webshop
    [Tags]                      Order History

        Go Order History Page

User can view Order from Order History
    [Documentation]             Test: User should be able to see order details from order history List
    [Tags]                      View order from Order History

    View order from Order History

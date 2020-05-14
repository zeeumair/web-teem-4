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
User can access Webshop page
    [Documentation]             See if page is accessable and can load correctly
    [Tags]                      Test 1
    Go to Web Page

User can switch to Shopping Cart
    [Documentation]             Test: The user should be able to Clear Shopping Cart from any products
    [Tags]                      Clear Shopping Cart
    Clear Shopping Cart
    Verify Clear Shopping Cart from Products
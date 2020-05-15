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
    [Documentation]             Test: The user should be able to switch to Shopping Cart page from the header menu
    [Tags]                      Shopping Cart
    Switch to Shopping Cart
    Verify Shopping Cart Loaded

User can add more products on Shopping Cart
    [Documentation]             Test: The user should be able to add more products on Shopping Cart
    [Tags]                      Adding more products
    Verify adding products

User can remove products from Shopping Cart
    [Documentation]             Test: The user should be able to remove products from Shopping Cart
    [Tags]                      Remove products
    Verify removing products

User Can Check the Back tab in clickable on Shopping Cart
    [Documentation]             Test: The user should be able to click on the "Back" tab to switch to the Webshop page
    [Tags]                      Back tab function
    Verify Back Tab Is Clickable




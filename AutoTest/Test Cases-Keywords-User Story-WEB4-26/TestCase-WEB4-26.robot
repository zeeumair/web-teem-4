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
${first_name} =                .........
${sur_name} =                  .........
${phone_number} =              .........

*** Test Cases ***
User can access Webshop page

    [Documentation]             See if page is accessable and can load correctly
    [Tags]                      Test 1

        Go to Web Page

User can switch to Shopping Cart
    [Documentation]             Test: The user should be able to switch to Shopping Cart page from the header menu
    [Tags]                      Shopping Cart

        Go to Web Page
        Switch to Shopping Cart

User can add more products on Shopping Cart
    [Documentation]             Test: The user should be able to add more products on Shopping Cart
    [Tags]                      Adding more products

        Click Button            xpath:/html/body/div/main/table/tbody/tr[1]/td[2]/div/div[1]/form/input[3]

User can remove products from Shopping Cart
    [Documentation]             Test: The user should be able to remove products from Shopping Cart
    [Tags]                      Remove products

        Click Button            xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]

User can empty the Shopping Cart
    [Documentation]             Test: The user should be able to empty the Shopping Cart from any products
    [Tags]                      Empty Shopping Cart

        Click Button             xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]

User Can Check the Back tab in clickable on Shopping Cart
    [Documentation]             Test: The user should be able to click on the "Back" tab to switch to the Webshop page
    [Tags]                      Back tab function

        Click Element           xpath:/html/body/div/main/a




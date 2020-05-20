*** Settings ***

Documentation                   This is some basic info about the whole OMGZ SHOES test suite
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
    [Documentation]             Test: User should be able to access the OMGZ SHOES main page
    [Tags]                      Access OMGZ SHOES

        Go to Web Page

User should be able to see list of all products
    [Documentation]             Test: The user should be able to see all products when click on webshop
    [Tags]                      Product List

         Click Element          xpath:/html/body/header/nav/div/a

User can see Product Details
    [Documentation]             Test: The user should be able to see product Information
    [Tags]                      Product Information

         Product Details
         Verify Details Product Page

User can add Products in Shopping Cart from Product List
    [Documentation]             Test: The user should be able to add products in shopping cart
    [Tags]                      Adding product

        Verify Adding Products in Shopping Cart





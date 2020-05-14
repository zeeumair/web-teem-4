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

User can Log in with valid E-mail
    [Documentation]             Test: The user should be able to Log in after being registered
    [Tags]                      Log in to User account
        Log in with Valid E-mail
        Verify Log in with valid E-mail

User can add Products in Shopping Cart from Product List
    [Documentation]             Test: The user should be able to add products in shopping cart
    [Tags]                      Adding product
        Adding Products in Shopping Cart
        Verify Adding Products in Shopping Cart

User can Conferm Order
    [Documentation]             Test: The user should be able Conferm Order
    [Tags]                      Conferm Order
        Conferm Order
        Varify Conferm Order

User can check Min Sida after being logged in with Valid E-mail
    [Documentation]             Test: The User should be able to check input detaild on Min Sida after log in
    [Tags]                      Check Min Sida
        Check Min Sida after succsess Log in
        Verify Check Min Sida contains correct registeration's input for this User


User can access Order History page
    [Documentation]             Test: User should be able to see order history on Webshop
    [Tags]                      Order History
        Go Order History Page
        Varify Order History Page

User can view Order from Order History
    [Documentation]             Test: User should be able to see order details from order history List
    [Tags]                      View order from Order History
        View order from Order History
        Varify order from Order Histroy

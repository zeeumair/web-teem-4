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
${email_text}                   alfred.josephalfred@gmail.com

*** Test Cases ***
User can access Webshop page
    [Documentation]             See if page is accessable and can load correctly
    [Tags]                      Access Webshop main page
    Go to Web Page

User can Log in with valid E-mail
    [Documentation]             Test: The user should be able to Log in after being registered
    [Tags]                      Log in to User account
    Log in with Valid E-mail
    Verify Log in with valid E-mail

User can switch to Shopping Cart
    [Documentation]             Test: The user should be able to switch to Shopping Cart page from the header menu
    [Tags]                      Shopping Cart
    Switch to Shopping Cart

User can Confirm an Order
    [Documentation]             Test: The User should be able to Confirm an Order by clicking on "confirm Order" button
    [Tags]                      Confirm Order
    Verify Confirm Order

User select any way of Payment and Delivery Option
    [Documentation]             Test: The User can choose way of Payment and Delivery Option
    [Tags]                      Way of Payment & Days of Delivery
    Verify way of Payment and Delivery Option to Confirm order

User can select Swish as way of Payment and Delivery Option to Confirm order then click on Shop More
    [Documentation]             Test: The User should be able to select Swish as way of payment & days of delivery
    [Tags]                      Select Swish
    Verify Swish Payment and Delivery Option to Confirm order then click on Shop More

User can select Invoice as way of Payment and Delivery Option to Confirm order then click on Download as PDF
    [Documentation]             Test: The User should be able to select Invoice as way of payment & days of delivery
    [Tags]                      Select Invoice
    Verify Invoice Payment and Delivery Option to Confirm order then click on Download as PDF
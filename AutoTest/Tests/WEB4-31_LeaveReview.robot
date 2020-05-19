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
    [Documentation]             Test: User should be able to access the Webshop main page
    [Tags]                      Access Webshop
        Go to Web Page

User can Log in with valid E-mail
    [Documentation]             Test: The user should be able to Log in after being registered
    [Tags]                      Log in to User account

    Log in with Valid E-mail

User can access Order History page
    [Documentation]             Test: User should be able to see order history on Webshop
    [Tags]                      Order History
        Go Order History Page

User can view Order from Order History
    [Documentation]             Test: User should be able to see order details from order history List
    [Tags]                      View order from Order History
        View order from Order History

User can give stars to a product
    [Documentation]             Test: User should be able to give atars 1 to 5 about a product quielty in product review
    [Tags]                      Give stars to product
        Give stars to a product
        Varify give stars to a product

User can leave review to a product
    [Documentation]             Test: User should be able to leave review about a product quielty in product review
    [Tags]                      Leave review to a product
        Leave review to a product

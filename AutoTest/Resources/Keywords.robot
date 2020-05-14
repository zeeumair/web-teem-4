
*** Settings ***

Library                             SeleniumLibrary

*** Keywords ***

Begin Web Test

    Open Browser                     about:blank  ${BROWSER}
    Set Selenium Speed               0.8

Go to Web Page
    Load page
    Maximize Browser Window
    Verify startpage loaded

Load page
    Go To                           ${URL}

Verify startpage loaded
    ${link_text} =                  Get Text  class:navbar-brand
    Should Be Equal                 ${link_text}  Webshop

Log in with Valid E-mail
    Click Link                      xpath:/html/body/header/nav/div/div/ul/li[2]/a[1]
    Input Text                      id:Email  zee.umair@hotmail.com
    Input Text                      id:Password  Onetwo34!
    Click Button                    xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Verify Log in with valid E-mail
    ${link_text} =                  Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a[1]
    Should Be Equal                 ${link_text}  Logout

Adding Products in Shopping Cart
    Click Element                   xpath:/html/body/div/main/table/tbody/tr[1]/td[6]/a[2]

Verify Adding Products in Shopping Cart
    Click Link                      Link:Shopping Cart
    ${link_text} =                  Get Text  xpath:/html/body/div/main/span/a
    Should Be Equal                 ${link_text}  Confirm Order

Conferm Order
    Click Link                      Link:Confirm Order
    Click Button                    xpath://*[@id="submit"]

Varify Conferm Order
    ${link_text} =                   Get Text  xpath:/html/body/div/main/a[1]
    Should Be Equal                  ${link_text}  ShopMore


Check Min Sida after succsess Log in
    Click Link                      xpath:/html/body/header/nav/div/div/ul/li[2]/a[2]
    Click Button                    xpath:/html/body/div/main/div/div/form/div[9]/input

Verify Check Min Sida contains correct registeration's input for this User
    ${link_text} =                  Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a[1]
    Should Be Equal                 ${link_text}  Logout


Go Order History Page
    Click Link                       Link: View Order History

Varify Order History Page
    Wait Until Page Contains         Order History
    ${link_text} =                   Get Text  xpath:/html/body/div/main/table/tbody/tr/td[6]/a
    Should Be Equal                  ${link_text}  View items

View order from Order History
    Click Link                       Link: View items

Varify order from Order Histroy
    ${link_text} =                   Get Text  xpath:/html/body/div/main/table/thead/tr/th[1]
    Should Be Equal                  ${link_text}  Name

End Web Test
    Close Browser



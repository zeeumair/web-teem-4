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
    Go To                            ${URL}

Verify startpage loaded
    ${link_text} =                   Get Text  class:navbar-brand
    Should Be Equal                  ${link_text}  OMGZ SHOES

Webshop products list
    Click Element                    xpath:/html/body/header/nav/div/a
    Wait Until Page Contains         Image

Verify Webshop
    ${link_text} =                   Get Text  xpath:/html/body/div/main/table/tbody/tr[1]/td[6]/a[2]
    Should Be Equal                  ${link_text}  Add to Shopping Cart

Product Details
    Click Link                       Link:Details

Verify Details Product Page
    ${link_text} =                   Get Text  xpath:/html/body/div/main/div/dl[1]/dd/a
    Should Be Equal                  ${link_text}  Add to Shopping Cart

Verify Adding Products in Shopping Cart
    Click Element                    xpath:/html/body/div/main/div/dl[1]/dd/a
    Click Link                       Link:Shopping Cart
    ${link_text} =                   Get Text  xpath:/html/body/div/main/div/span/a[2]
    Should Be Equal                  ${link_text}  Confirm Order

End Web Test
    Close Browser




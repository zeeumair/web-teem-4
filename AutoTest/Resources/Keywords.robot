
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
    Should Be Equal                  ${link_text}  Webshop

Go Order History Page
    Click Link                       Link: Order History
    Wait Until Page Contains         Order History
    sleep                            5s
    ${link_text1} =                  Get Text  xpath:/html/body/div/main/table/tbody/tr/td[6]/a
    Should Be Equal                  ${link_text1}  View items
    sleep                            5s

View order from Order History
    Click Link                       Link: View items
    ${link_text1} =                  Get Text  xpath:/html/body/div/main/table/thead/tr/th[1]
    Should Be Equal                  ${link_text1}  Name

End Web Test
    Close Browser



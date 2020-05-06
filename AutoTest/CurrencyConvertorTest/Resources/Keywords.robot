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

Varify Currency Converter Button
    ${link_text} =                   Get Text  xpath://*[@id="currentCurrency"]
    Should Be Equal                  ${link_text}  Currency:

Slect Currency from List
    Click Element                    Xpath://*[@id="14"]

Varify Selected Currency
    ${link_text} =                   Get Text  xpath://*[@id="currentCurrency"]
    Should Be Equal                  ${link_text}  Currency: EUR
    Wait                             4s

End Web Test
    Close Browser
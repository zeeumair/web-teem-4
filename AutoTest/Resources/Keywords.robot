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
    Should Be Equal                 ${link_text}    OMGZ SHOES

Log in with Valid E-mail
    Click Link                      xpath:/html/body/header/nav/div/div/ul/li[2]/a[1]
    Input Text                      id:Email  zee.umair@hotmail.com
    Input Text                      id:Password  Onetwo34!
    Click Button                    xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Go Order History Page
    Click Link                      xpath:/html/body/header/nav/div/div/ul/li[3]/a
    Execute Javascript              window.scrollBy(0,400)
    Click Link                      Id:OrderHistoryLink

View order from Order History
    Click Link                      Link:View items

Give stars to a product
    Click Element                   id:stars
    Click Element                   xpath://*[@id="stars"]/option[2]
    Click Button                    id:submitReview

Varify give stars to a product
    ${link_text} =                  Get Text  xpath://*[@id="stars"]/option[2]
    Should Be Equal                 ${link_text}  2

Leave review to a product
    Input Text                      id:description  Very confert
    Click Button                    id:submitReview

End Web Test
    Close Browser

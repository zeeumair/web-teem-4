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

Product without review and rating
    Click Link                      xpath://*[@id="2"]
    Execute Javascript              window.scrollBy(0,400)

Varify product without review and rating
    ${link_text} =                  Get Text  xpath:/html/body/div/main/div/p[2]
    Should Be Equal                 ${link_text}  There does not seem to be any reviews of this product yet

Product Details with all reviews
    Click Element                    xpath:/html/body/header/nav/div/a
    Click Link                       xpath://*[@id="1"]
    Execute Javascript               window.scrollBy(0,400)

Varify Product Details with all reviews
    ${link_text} =                  Get Text  xpath:/html/body/div/main/div/dl[3]/dd
    Should Be Equal                 ${link_text}  Very confert

Product Details with average rating
    Click Element                    xpath:/html/body/header/nav/div/a
    Click Link                       xpath://*[@id="1"]
    Execute Javascript               window.scrollBy(0,400)

Varify Product Details with average rating
    ${link_text} =                  Get Text  xpath:/html/body/div/main/div/dl[3]/dt
    Should Be Equal                 ${link_text}  2 / 5 Stars

End Web Test
    Close Browser
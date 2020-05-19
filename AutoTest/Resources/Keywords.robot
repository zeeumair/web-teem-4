*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window

    Set Selenium Speed          0.6

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  class:navbar-brand
    Should Be Equal		        ${link_text}  OMGZ SHOES

Random Chuck Norris joke before ordering
    Click Element               id:AddToShoppingCart
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[1]/a
    Click Link                  xpath:/html/body/div/main/div/span/a
    Input Text                  id:Email  alfred.josephalfred@gmail.com
    Input Text                  id:Password  Joe_777
    Click Element               xpath:/html/body/div/main/div/div[1]/div/div/form/div[4]/input
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[1]/a
    Click Link                  xpath:/html/body/div/main/div/span/a

Verify Chuck Norris joke
    ${link_text} =              Get Text  xpath://*[@id="jokeContainer"]/h3
    Should Be Equal             ${link_text}  Complimentary Chuck Norris Fact
End Web Test
    Close Browser


*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window

    Set Selenium Speed          0.5

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  xpath:/html/body/header/nav/div/a
    Should Be Equal		        ${link_text}  OMGZ SHOES

Login with Google Account
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Click Link                  xpath://*[@id="google-logo"]
    Input Text                  xpath://*[@id="identifierId"]  omgzshoezz@gmail.com
    Click Element               xpath://*[@id="identifierNext"]/span/span
    Input Text                  xpath://*[@id="password"]/div[1]/div/div[1]/input  OmgzOmgz123
    Click Element               xpath://*[@id="passwordNext"]/span/span

Verify Login with Google Account
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Should Be Equal             ${link_text}  Logout

End Web Test
    Close Browser
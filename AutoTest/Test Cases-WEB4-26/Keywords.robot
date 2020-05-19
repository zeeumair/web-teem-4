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
    ${link_text} = 		        Get Text  xpath:/html/body/header/nav/div/a
    Should Be Equal		        ${link_text}  OMGZ SHOES

Switch to Shopping Cart
     Go to Web Page
     Click Link                 xpath://*[@id="AddToShoppingCart"]
     Click Link                 xpath:/html/body/header/nav/div/div/ul/li[1]/a
     Wait Until Page Contains   Total cost:

Verify Shopping Cart Loaded
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/span/a
    Should Be Equal		         ${link_text}  Confirm Order

Verify adding products
    Click Button                 xpath:/html/body/div/main/div/table/tbody/tr/td[3]/div/div[1]/form/input[2]
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/table/thead/tr/th[3]
    Should Be Equal		         ${link_text}  Quantity

Verify removing products
    Click Button                xpath:/html/body/div/main/div/table/tbody/tr/td[3]/div/div[2]/form/input[2]
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/table/thead/tr/th[4]
    Should Be Equal		         ${link_text}  Price

Verify Back Tab Is Clickable
    Click Element               xpath:/html/body/div/main/a
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/a
    Should Be Equal             ${link_text}  OMGZ SHOES

End Web Test
    Close Browser

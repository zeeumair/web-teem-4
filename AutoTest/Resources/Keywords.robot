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

Clear Shopping Cart
     Go to Web Page
     Click Link                 xpath://*[@id="AddToShoppingCart"]
     Click Link                 xpath:/html/body/header/nav/div/div/ul/li[1]/a
     Click Element              id:clearButton

Verify Clear Shopping Cart from Products
    ${link_text} =              Get Text  xpath:/html/body/div/main/div/table/tfoot/tr/td/h4
    Should Be Equal             ${link_text}  Total cost: 0 SEK

End Web Test
    Close Browser
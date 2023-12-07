import org.junit.jupiter.api.*;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static org.junit.jupiter.api.Assertions.*;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
public class CRUDTest {
    private final WebDriver driver = new SeleniumConfig().getDriver();

    private final String baseUrl = "http://localhost:5118/Films";

    private final String createUrl = "/Create";
    private final String readUrl = "/Details/";
    private final String updateUrl = "/Edit/";
    private final String deleteUrl = "/Delete/";

    private final String film = "Nazwa";
    private final String updatedFilm = "Nazwa 2";
    private final String director = "Reżyser";
    private final String genre = "Gatunek";
    private final String rating = "9";
    private final String year = "1969";

    private static String filmId;

    @BeforeEach
    void setUp(){
        driver.get(baseUrl);
    }

    @AfterEach
    void tearDown(){
        driver.quit();
    }

    @Test
    @Order(1) // Create
    void givenFilmData_whenCreate_shouldFilmExits() throws Exception{
        fail();
        driver.get(baseUrl + createUrl);
        WebElement element = driver.findElement(By.name("Title"));
        element.sendKeys(film);

        element = driver.findElement(By.name("Director"));
        element.sendKeys(director);

        element = driver.findElement(By.name("Genre"));
        element.sendKeys(genre);

        element = driver.findElement(By.name("Rating"));
        element.sendKeys(rating);

        element = driver.findElement(By.name("Year"));
        element.sendKeys(year);

        element = driver.findElement(By.cssSelector("input[type='submit'][value='Create']"));
        element.click();
        Thread.sleep(1000);

        element = driver.findElement(By.linkText("Details"));
        Pattern pattern = Pattern.compile("/(\\d+)");
        Matcher matcher = pattern.matcher(element.getAttribute("href"));

        if (matcher.find())
            filmId = matcher.group(1);
        else
            fail();


        List<WebElement> list = driver.findElements(By.xpath("//*[contains(text(),'" + film + "')]"));
        assertEquals(1, list.size());
    }

    @Test
    @Order(2) // Read
    void givenFilmExists_whenGet_shouldFilmBeRetrieved() throws Exception{
        driver.navigate().to(baseUrl + readUrl + filmId);
        Thread.sleep(1000);

        List<WebElement> list = driver.findElements(By.xpath("//*[contains(text(),'" + film + "')]"));
        assertEquals(1, list.size());
    }

    @Test
    @Order(3) // Update
    void givenFilm_whenFilmDataChange_shouldFilmBeUpdate() throws Exception{
        driver.navigate().to(baseUrl + updateUrl + filmId);
        Thread.sleep(1000);

        WebElement element = driver.findElement(By.name("Title"));
        element.sendKeys(updatedFilm);

        element = driver.findElement(By.cssSelector("input[type='submit'][value='Update']"));
        element.click();
        Thread.sleep(1000);

        List<WebElement> list = driver.findElements(By.xpath("//*[contains(text(),'" + updatedFilm + "')]"));
        assertEquals(1, list.size());
    }

    @Test
    @Order(4) // Delete
    void givenFilm_whenDelete_shouldDeleteFilm() throws Exception {
        driver.get(baseUrl + deleteUrl + filmId);
        Thread.sleep(1000);

        driver.findElement(By.className("btn-danger")).click();
        Thread.sleep(1000);

        List<WebElement> films = driver.findElements(By.xpath("//*[contains(text(),'" + updatedFilm + "')]"));
        assertEquals(0, films.size());
    }
}

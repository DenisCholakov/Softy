package org.softuni;

import org.softuni.config.AppConfig;
import org.softuni.service.UserService;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

public class Main {
    public static void main(String[] args) {
//        ApplicationContext context = new ClassPathXmlApplicationContext("beans.xml");

//        ApplicationContext context = new AnnotationConfigApplicationContext(AppConfig.class);

        ApplicationContext context = new AnnotationConfigApplicationContext("org.softuni");

        UserService userService = context.getBean(UserService.class);
        UserService userService2 = context.getBean(UserService.class);

        System.out.println(userService.findYoungestUser().orElseThrow());
        System.out.println(userService.hashCode());
        System.out.println(userService2.hashCode());
        System.out.println(userService == userService2);
    }
}
package org.softuni.service;

import org.softuni.model.User;
import org.softuni.repository.UserRepository;
import org.springframework.beans.BeansException;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;
import org.springframework.stereotype.Service;

import java.util.Comparator;
import java.util.Optional;

@Service
public class UserServiceImpl implements UserService, ApplicationContextAware {
    private final UserRepository userRepository;

    public UserServiceImpl(@Qualifier("fileRepo") UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public Optional<User> findYoungestUser() {
        return userRepository.findAll().stream().min(Comparator.comparingInt(User::age));
    }

    @Override
    public void setApplicationContext(ApplicationContext applicationContext) throws BeansException {
        // here we can get new instance of prototype bean
        // other way is to make the class abstract
        // make a function getUserRepository and the framework would get us new repository
        // Spring method injection
    }
}

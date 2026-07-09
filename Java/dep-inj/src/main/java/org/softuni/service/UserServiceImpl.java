package org.service;

import org.model.User;
import org.repository.UserRepository;

import java.util.Comparator;
import java.util.Optional;

public class UserServiceImpl implements UserService{
    private final UserRepository userRepository;

    public UserServiceImpl(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public Optional<User> findYoungestUser() {
        return userRepository.findAll().stream().min(Comparator.comparingInt(User::age));
    }
}

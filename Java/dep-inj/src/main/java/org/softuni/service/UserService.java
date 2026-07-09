package org.service;

import org.model.User;

import java.util.Optional;

public interface UserService {
    Optional<User> findYoungestUser();
}

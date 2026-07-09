package org.repository;

import org.model.User;

import java.util.List;

public interface UserRepository {
    List<User> findAll();
}
